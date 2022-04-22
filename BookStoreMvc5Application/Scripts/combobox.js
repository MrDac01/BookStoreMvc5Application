'use strict';

class ComboBox {

    static defaultComboBoxSettings = {

        fillRelated: function (srcElemValue) {
            const mfr = this.mappingFillRelation || []; // mappingFillRelation
            //const map = Array.from(mfr).filter(s => s.name() == srcElem);
            if (mfr.length > 0) {
                Array.from(mfr).forEach((a) => a(srcElemValue));
            }
        },

        clearRelated: function () {
            const mcr = this.mappingClearRelation || []; // mappingFillRelation
            //const map = Array.from(mcr).filter(s => s.name() == srcElem);
            if (mcr.length > 0) {
                Array.from(mcr).forEach((a) => a());
            }
        },

        id: '',
        modelProperty: 'entities',
        valueProperty: 'id',
        displayProperty: 'description',
        registerChange: true,
        url: null,
        filter: null,
        currentValueProvider: null,

        mappingFillRelation: [],
        mappingClearRelation: [],
        groupParser: null,
        onSelectionChange: null,

        init: function () {

            if (this.id.length == 0)
                throw Error('ID элемента не установлен');

            const cbx = new ComboBox(this.id);
            this.cbx = cbx;
            const filter = typeof (this.filter) == 'function' ? this.filter() : this.filter;

            cbx.dataSource(d => d.ajaxJson(this.url, this.modelProperty).filter(filter))
                .value(this.valueProperty)
                .display(this.displayProperty)
                .groupParcer(this.groupParser);

            if (this.registerChange) {
                cbx.change((ev) => {
                    if (this.onSelectionChange != null && typeof (this.onSelectionChange) == 'function') {
                        this.onSelectionChange(ev, this, cbx);
                    }
                    const selectedValue = ev.currentTarget.value;
                    ev.preventDefault();
                    ev.stopPropagation();
                    if (selectedValue == '') {
                        this.clearRelated();
                    } else {
                        this.fillRelated();
                    }
                });
            }

            this.initialized = false;

            cbx.build().then((_) => {
                this.initialized = true;

                if (this.currentValueProvider != null && typeof this.currentValueProvider == 'function') {
                    const val = this.currentValueProvider();
                    this.setValue(val);
                }
            });
        },

        clear: function () {
            $(this.id).empty();

            $(this.id).change()
        },

        /**
         * Current data source
         * */
        dataSource: function () {
            return $(this.id).data('model');
        },

        dataSourceFind: function (predicate) {
            const ds = this.dataSource();
            return Array.from(ds).filter(predicate);
        },

        value: function () {
            let selected = $(this.id).one('option:selected').val();

            return selected;
        },

        disable: function () {
            $(this.id).attr('disabled', 'disabled')
        },

        hide: function () {
            $(this.id).parent().parent().hide()
        },

        enable: function () {
            $(this.id).removeAttr('disabled')
        },

        show: function () {
            $(this.id).parent().parent().show()
        },

        setValue: function (val) {
            $(this.id).val(val).change();
        }
    }

    constructor(id) {
        this.id = id;
    }

    dataSource(callback) {
        this.ds = new ComboBoxDataSource(this);
        callback.apply(this, [this.ds]);
        return this;
    }

    value(valueField) {
        this.valueField = valueField;
        return this;
    }

    display(displayField) {
        this.displayField = displayField;
        return this;
    }

    groupParcer(callback) {
        this._parseOptGroupCallback = callback;
        return this;
    }

    change(callback) {
        this._changeCallback = callback;
        return this;
    }

    build() {
        return new Promise((resolve, reject) => {

            this.addLoading();

            this.ds.getData().then((items) => {
                let combo = $(this.id);
                combo.data('model', items);
                combo.empty().end();
                combo.append(`<option selected="selected"></option>`);

                const ds = items || [];

                if (this._parseOptGroupCallback != null) {
                    const newDs = this._parseOptGroupCallback(items);
                    Array.from(newDs).forEach((group) => {
                        combo.append(`<optgroup label="${group.group}">`);

                        Array.from(group.items).forEach((p) => {
                            if (typeof (p) !== 'string') {
                                const value = p[this.valueField];
                                const display = p[this.displayField];
                                combo.append(`<option value="${value}">${display}</option>`);
                            } else {
                                combo.append(`<option value="${p}">${p}</option>`);
                            }
                        });

                        combo.append(`</optgroup>`);
                    })
                }
                else {
                    Array.from(ds).forEach((p) => {
                        if (typeof (p) !== 'string') {
                            const value = p[this.valueField];
                            const display = p[this.displayField];
                            combo.append(`<option value="${value}">${display}</option>`);
                        } else {
                            combo.append(`<option value="${p}">${p}</option>`);
                        }
                    })
                }

                if (this._changeCallback) {
                    combo.change(this._changeCallback);
                }

                resolve(items);
            }).catch((e) => {
                console.error(e);
                reject(e);
            }).finally(() => {
                setTimeout(() => {
                    this.removeLoading();
                }, 150)
            })
        })
    }

    addLoading() {
        $(this.id).parent().append(`<div class="loading-indicator">
    <div class="spinner-grow text-info spinner-border-sm" role="status">
        <span class="sr-only">Loading...</span>
    </div>
    <div class="spinner-grow text-info spinner-border-sm" role="status">
        <span class="sr-only">Loading...</span>
    </div>
    <div class="spinner-grow text-info spinner-border-sm" role="status">
        <span class="sr-only">Loading...</span>
    </div>
    <div class="spinner-grow text-info spinner-border-sm" role="status">
        <span class="sr-only">Loading...</span>
    </div>
</div>`);
    }

    removeLoading() {
        $(this.id).parent().find('.loading-indicator').remove();
    }
}

class ComboBoxDataSource {
    constructor(combobox) {
        this.combobox = combobox;
    }

    ajaxJson(url, modelField) {
        this.type = 'ajax';
        this.url = url;
        this.modelField = modelField;
        return this;
    }

    filter(filterValue) {
        this.filterValue = filterValue;
        return this;
    }

    getData() {
        return new Promise((resolve, reject) => {

            if (this.type == 'ajax') {
                $.getJSON(this.url, this.filterValue)
                    .done(data => {
                        const items = data[this.modelField];
                        resolve(items);
                    })
                    .fail(e => {
                        reject(e);
                    });
            } else {
                reject(Error('Не указан провайдер данных, или указанный провайдер не поддерживается'));
            }
        })
    }
}