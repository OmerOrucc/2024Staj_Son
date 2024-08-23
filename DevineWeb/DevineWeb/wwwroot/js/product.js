$(document).ready(function () {
    loadDataTable();
    toastr.info('Toastr kütüphanesi başarıyla yüklendi!');
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": { url: '/admin/product/getall' },
        "columns": [
            { data: 'title', "width": "15%" },
            { data: 'listPrice', "width": "15%" },
            { data: 'seller', "width": "15%" },
            { data: 'category.name', "width": "15%" },
            {
                data: 'id',
                "render": function (data) {
                    return `
            <div class="w-75 btn-group" role="group">
                <a href="/admin/product/upsert?id=${data}" class="btn btn-primary mx-2">
                    <i class="bi bi-pencil-square"></i> Edit
                </a>
                <a href="javascript:void(0);" data-url="/admin/product/delete/${data}" class="btn btn-danger mx-2 delete-btn">
                    <i class="bi bi-trash3-fill"></i> Delete    
                </a>
            </div>`;
                },
                "width": "15%"
            }
        ],
        "drawCallback": function (settings) {
            $('#tblData').off('click', '.delete-btn'); // Önceki olayları kaldır
            $('#tblData').on('click', '.delete-btn', function () {
                var url = $(this).data('url');
                Delete(url);
            });
        }
    });
}

function Delete(url) {
    console.log("Deleting URL: ", url); // URL'yi kontrol edin
    if (confirm('Are you sure you want to delete this record?')) {
        $.ajax({
            url: url, // Doğru URL burada kullanılır
            type: 'DELETE',
            success: function (result) {
                console.log(result); // Sunucudan gelen yanıtı konsola yazdır
                if (result.success) {
                    dataTable.ajax.reload();
                    toastr.success(result.message);
                } else {
                    toastr.error(result.message);
                }
            },
            error: function (err) {
                console.log(err); // Hataları konsola yazdır
                toastr.error('Error while deleting record');
            }
        });
    }
}
