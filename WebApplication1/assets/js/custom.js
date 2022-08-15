$(".edit").click(function (e) {
    e.preventDefault();

    var idt = $(this).attr("data-idtoy");

    //alert("Este es el id: " + idt)
    var name = $(this).parent().parent().find(".titletoy").text().trim();
    var desc = $(this).parent().parent().find(".desc").text().trim();
    var age = $(this).parent().parent().find(".age").text().trim();
    var price = $(this).parent().parent().find(".price").text().trim();
    var company = $(this).parent().parent().find(".company").text().trim();
    var imge = $(this).parent().parent().find("img").attr("src");

    $(".imgtoye").attr("src", imge);

    $(".nameedit").val(name);
    $(".descedit").val(desc);
    $(".ageedit").val(age);
    $(".priceedit").val(price);
    $(".companytedit").val(company);

    $("#saveupdateToy").attr("data-idToy", idt);

    $("#edittoy").modal("show");

});


$("#addtoybutton").click(function (e) {
    e.preventDefault();
    

    $("#addtoy").modal("show");

});

$(".inputContent input").focus(function () {

    $(this).parent().find("label").css({
        "bottom":"0",
        "font-size": "9px",
        "top": "0",
        "color": "#8e8efa",
        "transition": "all .3s"
    });
});
$(".inputContent input").focusout(function () {

    var valinput = $(this).val();
    if (valinput == "") {
        $(this).parent().find("label").css({
            "bottom": "12px",
            "font-size": "14px",
            "top": "auto",
            "color": "#459fb7",
            "transition": "all .3s"
        });
    }    
});


$(document).ready(function () {
    $("#searchtoy").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $("#listToys tr").filter(function () {
            $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
        });
    });
});


$("#saveupdateToy").click(function (e) {
    e.preventDefault();
    var idT = $(this).attr("data-idToy");
    var formData = new FormData();

    //var img1 = $("#imgtoy").get(0).files[0];
    var name = $(".nameedit").val();
    var desc = $(".descedit").val();
    var age = $(".ageedit").val();
    var price = $(".priceedit").val();
    var company = $(".companytedit").val();

    var inps = $(".inpaddup");
    var isempty = false;

    for (var i = 0; i < inps.length; i++) {
        if ($(inps[i]).val() == "") {
            $(inps[i]).addClass("inpempty");
            isempty = true;
        }
        else {
            $(inps[i]).removeClass("inpempty");
        }
    }

    if (isempty) {
        return;
    }


    //formData.append("img1", img1);
    formData.append("name", name);
    formData.append("desc", desc);
    formData.append("age", age);
    formData.append("price", price);
    formData.append("company", company);
    formData.append("idT", idT);

    $.ajax({

        type: "POST",
        //url: "Home/updateToy",
        url: "updateToy",
        data: formData,
        cache: false,
        contentType: false,
        processData: false,
        beforeSend: function () {
            $(".spinner").css("display", "block");
        },
        success: function (partialViewResult) {

            $(".spinner").css("display", "none");

            $("#AddCustomers").modal("hide");

            //$("#tableInfo").html(partialViewResult);

            location.reload();

        },
        error: function () {

            $(".spinner").css("display", "none");
        }
    });
})

$("#saveToy").click(function () {

    var formData = new FormData();
    var img1 = $("#imgtoy").get(0).files[0];
    var name = $(".namet").val();
    var desc = $(".desct").val();
    var age = $(".aget").val();
    var price = $(".pricet").val();
    var company = $(".companyt").val();    
    var inps = $(".inpadd");
    var isempty = false;
    
    for (var i = 0; i < inps.length; i++)
    {
        if ($(inps[i]).val() == "") {
            $(inps[i]).addClass("inpempty");
            isempty = true;
        }
        else {
            $(inps[i]).removeClass("inpempty");            
        }
    }

    if (img1 == undefined) {
        $("#imgtoy").addClass("inpempty");
        isempty = true;
    }
    else {
        $("#imgtoy").removeClass("inpempty");   
    }

    if (isempty) {
        return;
    }

    

    formData.append("img1", img1);
    formData.append("name", name);
    formData.append("desc", desc);
    formData.append("age", age);
    formData.append("price", price);
    formData.append("company", company);

    $.ajax({

        type: "POST",
        //url: "Home/AddToy",
        url: "AddToy",
        data: formData,
        cache: false,
        contentType: false,
        processData: false,
        beforeSend: function () {
            $(".spinner").css("display", "block");
        },
        success: function (partialViewResult) {

            $(".spinner").css("display", "none");

            $("#AddCustomers").modal("hide");

            //$("#tableInfo").html(partialViewResult);

            location.reload();

        },
        error: function () {

            $(".spinner").css("display", "none");
        }
    });
})

$(".deleteToy").click(function () {

    var idt = $(this).attr("data-idtoy");
    $("#deletetoyyes").attr("data-idToy", idt);
    var name = $(this).parent().parent().find(".titletoy").text().trim();
    $(".nametoy").text(name);
    $("#deletetoy").modal("show");
});

$("#deletetoyyes").click(function () {

    var idt = $(this).attr("data-idtoy");
    var formData = new FormData();

    formData.append("idt", idt);
    

    $.ajax({

        type: "POST",
        //url: "Home/deletetoy",
        url: "deletetoy",
        data: formData,
        cache: false,
        contentType: false,
        processData: false,
        beforeSend: function () {
            $(".spinner").css("display", "block");
        },
        success: function (partialViewResult) {

            $(".spinner").css("display", "none");

            $("#AddCustomers").modal("hide");

            //$("#tableInfo").html(partialViewResult);

            location.reload();

        },
        error: function () {

            $(".spinner").css("display", "none");
        }
    });
})

$(".ageedit").keyup(function () {

    if ($(this).val() > 100) {
        $(this).val(100)
    }
    else if ($(this).val() < 0) {
        $(this).val(0)
    }

});

$(".aget").keyup(function () {
    if ($(this).val() > 100) {
        $(this).val(100)
    } else if ($(this).val() < 0) {
        $(this).val(0)
    }
});



$(".pricet").keyup(function () {

    if ($(this).val() > 1000) {
        $(this).val(1000)
    }
    else if ($(this).val() < 1) {
        $(this).val(1)
    }

});
$(".priceedit").keyup(function () {
    if ($(this).val() > 1000) {
        $(this).val(1000)
    }
    else if ($(this).val() < 1) {
        $(this).val(1)
    }
});
