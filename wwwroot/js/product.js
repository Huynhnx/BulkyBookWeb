var dataTable;
$(document).ready(function () {
    loadDataTable();
})
function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url":"/Admin/Product/GetAll"
        },
        "columns": [
            { "data":"title","width":"15%"},
            {"data":"isbn","width":"15%"},
            {"data":"price","width":"15%"},
            {"data":"author","width":"15%"},
            { "data": "category.name", "width": "15%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="w-100 btn-btn-group" role ="group">
                            <a href="/Admin/Product/Upsert?id=${data}"
                             class="btn-primary w-50 p-1">
                                <i class="bi bi-pencil-square"></i> Edit
                            </a>
                            <a class="btn-danger w-50 p-1">
                                <i class="bi bi-trash-fill"></i> Delete </a>
                    </div>`
                },
                "width": "15%"
            }
        ]

    });
}
