(function () {
    'use strict'

    feather.replace()

    // Graphs
    var ctx = document.getElementById('myChart')
    // eslint-disable-next-line no-unused-vars
    var myChart = new Chart(ctx, {
        type: 'line',
        data: {
            labels: [
                'Sunday',
                'Monday',
                'Tuesday',
                'Wednesday',
                'Thursday',
                'Friday',
                'Saturday'
            ],
            datasets: [{
                data: [
                    15339,
                    21345,
                    18483,
                    24003,
                    23489,
                    24092,
                    12034
                ],
                lineTension: 0,
                backgroundColor: 'transparent',
                borderColor: '#007bff',
                borderWidth: 4,
                pointBackgroundColor: '#007bff'
            }]
        },
        options: {
            scales: {
                yAxes: [{
                    ticks: {
                        beginAtZero: false
                    }
                }]
            },
            legend: {
                display: false
            }
        }
    })
}())

let row;

$(document).ready(function ($) {
    $(".table-row").click(function () {
        row = $(this);
        var cells = $(this).children();
        $("#hiddenId").val(cells[0].innerHTML);
        $("#title").val(cells[1].innerHTML);
        $("#type").val(cells[2].innerHTML);
        $("#vendorCode").val(cells[3].innerHTML);
        $("#brandId").val(cells[5].innerHTML);
        $("#retailPrice").val(cells[6].innerHTML);
        $("#categoryId").val(cells[7].innerHTML);
        $("#packageId").val(cells[8].innerHTML);
        $("#countInStorage").val(cells[9].innerHTML);
        $("#warrantyMonth").val(cells[10].innerHTML);
        $("#series").val(cells[11].innerHTML);
        $("#model").val(cells[12].innerHTML);
        $("#descriptionTextarea").val(cells[4].innerHTML);
    });
});

$("#editProductBtn").click(function () {

    var id = $("#hiddenId").val();
    //row = $("tr").has("td[value='" + id + "']");
    let cells = $(row).children();

    cells[1].innerHTML = $("#title").val();
    cells[2].innerHTML = $("#type").val();
    cells[3].innerHTML = $("#vendorCode").val();
    cells[5].innerHTML = $("#brandId").val();
    cells[6].innerHTML = $("#retailPrice").val();
    cells[7].innerHTML = $("#categoryId").val();
    cells[8].innerHTML = $("#packageId").val();
    cells[9].innerHTML = $("#countInStorage").val();
    cells[10].innerHTML = $("#warrantyMonth").val();
    cells[11].innerHTML = $("#series").val();
    cells[12].innerHTML = $("#model").val();
    cells[4].innerHTML = $("#descriptionTextarea").val();
});


$("#uploadBtn").click(function () {
    let rows = $('tbody').children('.table-row');
    if (rows.length > 0) {
        let productArray = [];

        for (let i = 0; i < rows.length; i++) {
            productArray.push({
                Id: rows[i].children[0].innerHTML,
                Title: rows[i].children[1].innerHTML,
                Type: rows[i].children[2].innerHTML,
                VendorCode: rows[i].children[3].innerHTML,
                BrandId: rows[i].children[5].innerHTML,
                RetailPrice: rows[i].children[6].innerHTML,
                CategoryId: rows[i].children[7].innerHTML,
                PackageId: rows[i].children[8].innerHTML,
                CountInStorage: rows[i].children[9].innerHTML,
                WarrantyMonth: rows[i].children[10].innerHTML,
                Series: rows[i].children[11].innerHTML,
                Model: rows[i].children[12].innerHTML,
                DescriptionTextarea: rows[i].children[4].innerHTML
            });
        }

        $.ajax({
            type: 'POST',
            url: '/Admin/UploadProducts',
            data: { products: JSON.stringify(productArray) }
        });

    }
});




    $.ajax({
        url: "AddCharacteristic",
        type: "Get",
        success: function (response) {
            $("#charcteristicContainer").html(response);
        }
    });




