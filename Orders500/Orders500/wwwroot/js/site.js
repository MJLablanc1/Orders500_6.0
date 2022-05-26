

$(document).ready(function () {


    // Send an AJAX request for sales persons
    $.getJSON('api/<OrderController>/orders')
        .done(function (data) {
            // On success, 'data' contains a list of products.
            let salesArray = data;
            let salesSelectElement = document.getElementById("OrderTable");
            salesArray.forEach(function (value) {
                salesSelectElement.appendChild(new Option(value.last, value.salesPersonId));
            });

        });

    // Send an AJAX request for store data
    $.getJSON('api/<OrderController>/store')
        .done(function (data) {
            // On success, 'data' contains a list of products.
            let storeArray = data;
            let storeSelectElement = document.getElementById("storeSelect");
            storeArray.forEach(function (value) {
                storeSelectElement.appendChild(new Option(value.last, value.salesPersonId));
            });

        });

    // Send an AJAX request for cd data
    $.getJSON('api/<OrderController>/cds')
        .done(function (data) {
            // On success, 'data' contains a list of products.
            let cdsArray = data;
            let cdsSelectElement = document.getElementById("cdsSelect");
            cdsArray.forEach(function (value) {
                cdsSelectElement.appendChild(new Option(value.last, value.salesPersonId));
            });

        });

    // Send an AJAX request for sales persons data
    $.getJSON('api/<OrderController>/sales')
        .done(function (data) {
            // On success, 'data' contains a list of products.
            let salesArray = data;
            let salesSelectElement = document.getElementById("OrderTable");
            salesArray.forEach(function (value) {
                salesSelectElement.appendChild(new Option(value.last, value.salesPersonId));
            });

        });


    // Send an AJAX request for sales persons data
    $.getJSON('api/<OrderController>/')
        .done(function (data) {
            // On success, 'data' contains a list of products.
            let salesArray = data;
            let salesSelectElement = document.getElementById("OrderTable");
            salesArray.forEach(function (value) {
                salesSelectElement.appendChild(new Option(value.last, value.salesPersonId));
            });

        });




    // Send an AJAX request for count data
    $.getJSON('api/Order')   // note plural bird
        .done(function (data) {
            // On success, 'data' contains a list of products.
            let orderArray = data;
            drawTable(orderArray);
        });

});






function find() {
    var id = $('#CountId').val();
    $.getJSON('api/Birds' + '/' + id)
        .done(function (data) {
            console.log(data);
            $('#description').text(data);
        })
        .fail(function (jqXHR, textStatus, err) {
            $('#description').text('Error: ' + err);
        });
}



function drawTable(orderArray) {
    // get the reference for the table
    // creates a <table> element
    var tbl = document.getElementById('orderTable');
    while (tbl.rows.length > 1) {  // clear, but don't delete the header
        tbl.deleteRow(1);
    }

    // creating rows
    for (var r = 0; r < birdArray.length; r++) {
        var row = document.createElement("tr");

        var cell0 = document.createElement("td");
        var cell1 = document.createElement("td");
        var cell2 = document.createElement("td");
        var cell3 = document.createElement("td");
        var cell4 = document.createElement("td");
        tbl.appendChild(row); // add the row to the end of the table body
    }

}



function addOrder() {
    // 53	Bushtit	10	North Pierce County
    // "A205", "A1", 2, 77

    let selectsales = document.getElementById('salesSelect');
    let salesvalue = selectsales.options[selectRegion.selectedIndex].value;

    let selectstores = document.getElementById('storesSelect');
    let storesvalue = selectstores.options[selectBird.selectedIndex].value;

    let selectcd = document.getElementById('cdSelect');
    let cdvalue = selectcd.options[selectBirder.selectedIndex].value;

    let orderCount = parseInt(document.getElementById('howmany').value);



    let newOrder = new Order(storesvalue, salesvalue, 'cdprice', cdvalue, orderCount);
    console.log(newOrder);
    $.ajax({
        url: "api/Orders",
        type: "POST",
        data: JSON.stringify(newOrder),
        contentType: "application/json; charset=utf-8",

        success: function (result) {
            alert(result + " was added");
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert("Status: " + textStatus); alert("Error: " + errorThrown);
        }
    });
}

// 53	Bushtit	10	North Pierce County
// "A205", 2, "A1" 77
let Event = function (pRegionId, pBirderId, pBirdId, pCounted) {
    this.RegionId = pRegionId;
    this.BirderId = parseInt(pBirderId);
    this.BirdId = pBirdId;
    this.Counted = parseInt(pCounted);
}



