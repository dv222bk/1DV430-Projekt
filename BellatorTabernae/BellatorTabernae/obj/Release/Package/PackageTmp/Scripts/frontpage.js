$(document).ready(function () {
    var navigation = $('.nav-collapse').eq(0);
    var login = $('#MainPlaceHolder_LoginPanel');
    var newUser = $('#MainPlaceHolder_NewUserPanel');
    var frontPagePanel = $('#MainPlaceHolder_FrontPageMainPanel');

    var mql = window.matchMedia("all and (min-width: 40em)");

    if (!mql.matches) {
        $(navigation).find("ul").append(login);
        $(login).wrap("<li></li>");

        $(navigation).find("ul").append(newUser);
        $(newUser).wrap("<li></li>");
    }

    mql.addListener(function (e) {
        if (!e.matches) {
            $(navigation).find("ul").append(login);
            $(login).wrap("<li></li>");

            $(navigation).find("ul").append(newUser);
            $(newUser).wrap("<li></li>");
        } else {
            $(newUser).unwrap();
            $(frontPagePanel).append(newUser);

            $(login).unwrap();
            $(frontPagePanel).append(login);
        }
    });
});