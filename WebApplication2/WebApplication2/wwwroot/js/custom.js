showInPopup = (url, title) => {
    $.ajax({
        type: 'GET',
        url: url,
        success: function (res) {
            $('#form-modal .modal-body').html(res);
            $('#form-modal .modal-title').html(title);
            $('#form-modal').modal('show');
            
            // to make popup draggable
            $('.modal-dialog').draggable({
                handle: ".modal-header"
            });
        }
    })
}

var Delete = function (button) {
    // $('table.display').on("click", ".btnDelete", function () {
    var btn = button;

    swal({
        title: 'Silme Onayi',
        text: 'Bu Kaydi Silmek Istiyormusunuz?',
        type: 'warning',
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: 'Kaydi Sil',
        closeOnConfirm: true,
        cancelButtonText: "Kapat"

    }, function (isConfirm) {
        if (isConfirm) {
            $.ajax({
                url: '/' + btn.attr("data-url"),
                data: {
                    id: btn.data("id")
                },
                type: "post",
                cache: false,
                success: function (result) {
                    ShowMessage(result);
                    btn.parent().parent().remove();
                },
                error: function (xhr, ajaxOptions, thrownError) {

                }
            });
        }
    });
}
var SaveControlAndRefresh = function (response) {
    if (response.Messages != null) {
        ShowMessage(response);
        RefreshGrid();
    }
}
var SaveControl = function (response) {
   
    if (response["messages"] != null) {
        ShowMessage(response);
        if (response["actionName"] != null && response["targetId"] != "") {
            $.ajax({
                url: response["actionName"],
                success: function (result) {
                    RefreshGrid();
                }
            });
        }
        else if (response.ActionName != null && response.ActionName != "" && response.TargetId == "") {
            window.location.href = '/' + response.ActionName;
        }
    }
}
var Onay = function (button) {

    var btn = button;

    swal({
        title: 'Onay Durumu',
        text: 'Bu Talebi Onaylamak Istiyor musunuz?',
        type: 'info',
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: 'Onayla',
        closeOnConfirm: true,
        cancelButtonText: "Kapat"

    }, function (isConfirm) {
        if (isConfirm) {
            $.ajax({
                url: '/' + btn.attr("data-url"),
                data: {
                    id: btn.data("id")
                },
                type: "post",
                cache: false,
                success: function (result) {
                    ShowMessage(result);
                    btn.parent().parent().remove();
                },
                error: function (xhr, ajaxOptions, thrownError) {

                }
            });
        }
    });
}

var ShowMessage = function (response) {
    if (response == null)
        return;

    if (response["messages"] == null)
        return;

    if (response["messageType"] == 0) {
        var options = { 'closeButton': false, 'debug': false, 'newestOnTop': true, 'progressBar': false, 'positionClass': 'toast-top-right', 'preventDuplicates': false, 'onclick': null, 'showDuration': '300', 'hideDuration': '1000', 'timeOut': '3000', 'extendedTimeOut': '1000', 'showEasing': 'swing', 'hideEasing': 'linear', 'showMethod': 'fadeIn', 'hideMethod': 'fadeOut' }

        for (i = 0; i < response["messages"].length; i++) {
            if (response["messageAction"] == 0) {
                toastr.success(response["messages"][i], '', options);
                if (response["closeModal"] == true)
                    jQuery(document.getElementsByClassName('modal')).modal('hide');
            }
            else
                toastr.warning(response["messages"][i], '', options);
        }
    }

    if (response["messageType"] == 1) {
        var status = "success";
        if (response["messageAction"] == 1)
            status = "info";
        else if (response["messageAction"] == 2)
            status = "warning";
        else if (response["messageAction"] == 3)
            status = "error";
        if (response["closeModal"] == true)
            jQuery(document.getElementsByClassName('modal')).modal('hide');
        swal({ title: response["title"], text: response["messages"][0], type: status, showCancelButton: false, confirmButtonText: 'Kapat', closeOnConfirm: true }, function () { });
    }
};