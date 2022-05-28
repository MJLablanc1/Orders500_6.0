let g_currentStore = 98007;

function PopulateStoreDropdown(dropdownID) {
    $.getJSON('api/order/store')
        .done(function (data) {
            let output = "";
            for (let entry in data) {
                output += `<option value='${data[entry].storeID}' onclick='PopulateEmployeeDropdown("employee-order", ${data[entry].storeID})'>${data[entry].city}</option>`
            }

            const storeDropdown = document.getElementById(dropdownID);
            storeDropdown.innerHTML = output;
        })
        .fail(function (jqXHR, textStatus, err) {
            outputDiv.innerHTML = output; 'Error: ' + err;
        });
}

function PopulateEmployeeDropdown(dropdownID, storeId) {
    $.getJSON(`api/order/sales/${storeId}`)
        .done(function (data) {
            g_currentStore = storeId;
            let output = "";
            for (let entry in data) {
                output += `<option value='${data[entry].salesPersonId}'>${data[entry].first} ${data[entry].last}</option>`
            }

            const employeeDropdown = document.getElementById(dropdownID);
            employeeDropdown.innerHTML = output;
        });
}

function PopulateCdDropdown(dropdownID) {
    $.getJSON('api/order/cds')
        .done(function (data) {
            let output = "";
            for (let entry in data) {
                output += `<option value='${data[entry].cdID}'>${data[entry].cdName}: ${data[entry].listPrice}</option>`
            }

            const storeDropdown = document.getElementById(dropdownID);
            storeDropdown.innerHTML = output;
        })
        .fail(function (jqXHR, textStatus, err) {
            const outputDiv = document.getElementById("output-div");
            outputDiv.innerHTML = output; 'Error: ' + err;
        });

}

function GetHighestSales() {
    $.getJSON('api/order/highest')
        .done(function (data) {
            let output = "<table><tr><th>Store</th><th>Number of Sales</th></tr>";
            for (let entry in data) {
                output += `<tr><td>${data[entry].city}</td><td>${data[entry].salesNumber}</td></tr>`
            }
            output += '</table>'
            const outputDiv = document.getElementById("output-div");
            outputDiv.innerHTML = output;
        })
        .fail(function (jqXHR, textStatus, err) {
            const outputDiv = document.getElementById("output-div");
            outputDiv.innerHTML = output; 'Error: ' + err;
        });
}

function GetPerformance() {
    $.getJSON(`api/order/preformance/${g_currentStore}`)
        .done(function (data) {
            let output = "<table><tr><th>Store</th><th>Total Money Earned</th></tr>";
            output += `<tr><td>${data.cityName}</td><td>${data.totalSalesAmount}</td></tr>`;
            output += '</table>'
            const outputDiv = document.getElementById("output-div");
            outputDiv.innerHTML = output;
        })
        .fail(function (jqXHR, textStatus, err) {
            const outputDiv = document.getElementById("output-div");
            outputDiv.innerHTML = output; 'Error: ' + err;
        });
}

function SubmitOrder() {
    const store = document.getElementById("store-order").value;
    const salesPerson = document.getElementById("employee-order").value;
    const cd = document.getElementById("cd-order").value;

    $.post(`api/order/post/${store}/${salesPerson}/${cd}`)
        .done(function () {
            let output = `New Order Submitted: ${store}, ${salesPerson}, ${cd}`;
            const outputDiv = document.getElementById("output-div");
            outputDiv.innerHTML = output;

        });
}

$(document).ready(function () {
    PopulateStoreDropdown('store-order');
    PopulateEmployeeDropdown('employee-order', 98007);
    PopulateCdDropdown('cd-order');
});