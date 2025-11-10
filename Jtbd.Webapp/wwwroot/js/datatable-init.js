// wwwroot/js/datatable-init.js

window.initializeDataTables = function (tableId) {
    // Initialize DataTable with responsive true
    $('#' + tableId).DataTable({
        responsive: true,
        paging: true,
        searching: true,
    });
};