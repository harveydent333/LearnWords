function deleteSelectedItems() {
    $.ajax({
        type: "POST",
        url: "/Product/DeleteSelected/",
        data: { ids: Array.from(selected) },
        traditional: true,
        dataType: 'html',
        success: function (data) {
            console.log('success', arguments);
            window.document.write(data);
        },
        error: function () {
            console.log('error', arguments);
        }
    });
}

function updateSelectedItems() {
    $.ajax({
        type: "GET",
        url: "/Product/UpdateSelected/",
        data: { ids: Array.from(selected) },
        dataType: 'html',
        traditional: true,
        success: function (data) {
            console.log('success', arguments);
            window.history.replaceState(data, "Update Products", "/Product/UpdateSelected");
            window.document.write(data);
        },
        error: function () {
            console.log('error', arguments);
        }
    });
}