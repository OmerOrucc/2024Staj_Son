function NihaiOnayla(talepNo) {
    var frm = $('#frmServers');
    $.ajax({
        type: "POST",
        url: "/Demands/GenelSekreterOnayi",
        data: { id: talepNo },
        success: function (response) {

            if (response == 0) {
                swal({
                    type: 'warning',
                    title: "Onay Durumu",
                    text: "Bu talep genel sekreter onayýnda bekleme aþamasýndadýr.",
                    icon: "success",
                    confirmButtonText: "Tamam"
                });
            }
            if (response == 2) {
                swal({
                    type: 'warning',
                    title: "Onay Durumu",
                    text: "Bu talep genel sekreter onayýna gönderilmiþ ve reddedilmiþtir. Onaylanamaz",
                    icon: "success",
                    confirmButtonText: "Tamam"
                });
            }
            if (response == 1 || response == 3 ) {
                swal({
                    title: 'Onay Durumu',
                    text: 'Bu Talebi Nihai Olarak Onaylamak Istiyor musunuz?',
                    type: 'success',
                    showCancelButton: true,
                    confirmButtonColor: "#DD6B55",
                    confirmButtonText: 'Onayla',
                    closeOnConfirm: true,
                    cancelButtonText: "Kapat"
                }, function (isConfirm) {
                    if (isConfirm) {
                        $.ajax({
                            type: "POST",
                            url: "/Demands/TalepNihaiOnay",
                            data: frm.serialize(),
                            success: function (response) {
                                SaveControl(response);
                            }
                        });
                    }
                });
            }
        }
    });
}

function NihaiRed(talepNo) {
    var frm = $('#frmServers');
    $.ajax({
        type: "POST",
        url: "/Demands/GenelSekreterOnayi",
        data: { id: talepNo },
        success: function (response) {

            if (response == 0) {
                swal({
                    type: 'warning',
                    title: "Onay Durumu",
                    text: "Bu talep genel sekreter onayýnda bekleme aþamasýndadýr.",
                    icon: "success",
                    confirmButtonText: "Tamam"
                });
            }
            if (response == 1) {
                swal({
                    type: 'warning',
                    title: "Onay Durumu",
                    text: "Bu talep genel sekreter onayýna gönderilmiþ ve onaylanmýþtýr. Reddedilemez",
                    icon: "success",
                    confirmButtonText: "Tamam"
                });
            }
            if (response == 2 || response == 3) {
                swal({
                    title: 'Onay Durumu',
                    text: 'Bu Talebi Nihai Olarak Reddetmek Istiyor musunuz?',
                    type: 'warning',
                    showCancelButton: true,
                    confirmButtonColor: "#DD6B55",
                    confirmButtonText: 'Reddet',
                    closeOnConfirm: true,
                    cancelButtonText: "Kapat"
                }, function (isConfirm) {
                    if (isConfirm) {
                        $.ajax({
                            type: "POST",
                            url: "/Demands/TalepNihaiRed",
                            data: frm.serialize(),
                            success: function (response) {
                                SaveControl(response);
                                ShowMessage(result);
                            }
                        });
                    }
                });
            }
        }
    });
}

