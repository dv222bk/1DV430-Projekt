var navigation;
var login;
var newUser;
var frontPagePanel;

function responsiveLayout() {
    var windowWidth = $("html").css("width").replace("px", "");
    windowWidth = parseFloat(windowWidth) + 17;

    if (windowWidth < emToPx(40)) {
        if ($(navigation).find(login).length <= 0) {
            $(navigation).find("ul").append(login);
            $(login).wrap("<li></li>");
        }
        if ($(navigation).find(newUser).length <= 0) {
            $(navigation).find("ul").append(newUser);
            $(newUser).wrap("<li></li>");
        }
    } else {
        if ($(navigation).find(newUser).length >= 0) {
            $(newUser).unwrap();
            $(frontPagePanel).append(newUser);
        }
        if ($(navigation).find(login).length >= 0) {
            $(login).unwrap();
            $(frontPagePanel).append(login);
        }
    }
}

function emToPx(em) {
    var defaultEmSize = parseFloat($("body").css("font-size"));
    return em * defaultEmSize;
}

$(document).ready(function () {
    navigation = $('.nav-collapse').eq(0);
    login = $('#MainPlaceHolder_LoginPanel');
    newUser = $('#MainPlaceHolder_NewUserPanel');
    frontPagePanel = $('#MainPlaceHolder_FrontPageMainPanel');

    responsiveLayout();

    $(window).resize(function () {
        responsiveLayout();
    })
});