"use strict";
function kendoWindowDefaultOptions(options) {
    return $.extend({}, {
        modal: true,
        iframe: false,
        draggable: true,
        scrollable: true,
        pinned: false,
        title: 'Window',
        resizable: false,
        width: 900,
        height: 350,
        actions: ['Close'],
        visible: false
    }, options);
}
exports.kendoWindowDefaultOptions = kendoWindowDefaultOptions;
//# sourceMappingURL=utils.js.map