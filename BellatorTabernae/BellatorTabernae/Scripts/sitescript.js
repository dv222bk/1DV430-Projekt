$(document).ready(function () {
    var nav = responsiveNav(".nav-collapse", { // Selector
        label: "Meny" // String: Label for the navigation toggle
    });

    $(".EquipmentName").click(function () {
        $(this).parent().children("ul").toggle();
    });
});