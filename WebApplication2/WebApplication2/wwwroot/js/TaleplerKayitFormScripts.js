function OnTalepBasligi() {

    $.ajax({
        type: "POST",
        url: "/Demands/TalepBaslikDetay",
        data: {
            "Id": $("#ddlTalepBaslik").val(),
        },
        success: function (response) {

            document.getElementById('VarsayilanTerminSuresi').value = (response["terminSuresi"]);
            document.getElementById('VarsayilanMaliyetKayitFormu').value = (response["maliyet"]);
            document.getElementById('VarsayilanTeslimSuresi').value = (response["gerceklestirmeSuresi"]);
            document.getElementById('ddMiktartipiId').value = (response["miktartipi"]);
            document.getElementById("MiktarKayitFormu").readOnly = false;
            if (response["talepDurum"] == false) { document.getElementById("Miktar").readOnly = true; }

        }
    });
}
$('#VarsayilanMaliyetKayitFormu').inputmask('decimal', {
    alias: 'currency',
    radixPoint: ',',
    groupSeparator: '.',
    autoGroup: true,
    required: true,
    digits: 2,
    digitsOptional: false,
    placeholder: '0',
    min: 0,
    max: 99999,
    precision: 2,
    prefix: ""
});
$('#MiktarKayitFormu').inputmask('decimal', {
    alias: 'currency',
    radixPoint: ',',
    groupSeparator: '.',
    autoGroup: true,
    required: true,
    digits: 2,
    digitsOptional: false,
    placeholder: '0',
    min: 0,
    max: 99999,
    precision: 2,
    prefix: ""
});
