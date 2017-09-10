$(document).ready(function () {
    $('#tableData').DataTable({
        "ajax": {
            "url": "/home/loaddata",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "Unit.UnitName", "autoWidth": true },
            { "data": "EmployeeName", "autoWidth": true },
            { "data": "EmployeePosition", "autoWidth": true },
            { "data": "EmployeeHireDate", "autoWidth": true }

        ]
    });
});
function getName(str) {
    if (str.lastIndexOf('\\')) {
        var i = str.lastIndexOf('\\') + 1;
    }
    else {
        var i = str.lastIndexOf('/') + 1;
    }
    var filename = str.slice(i);
    var uploaded = document.getElementById("fileformlabel");
    uploaded.innerHTML = filename;
}