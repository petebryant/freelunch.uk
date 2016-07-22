//TODO only set focus to first visible field if there are no validation errors
$(document).ready(function () {
	    $(this).find('input:text:visible:first').focus();
});

$(document).ready(function () {
	$('.modal').on('shown.bs.modal', function () {
		$(this).find('input:text:visible:first').focus();
	})
});

$(document).on("keypress", ":input:not(textarea):not([type=submit])", function (event) {
    if (event.keyCode == 13) {
        event.preventDefault();
    }
});

$(document).ready(function () {
    $.validator.unobtrusive.adapters.addBool("booleanrequired", "required");
});

(function (i, s, o, g, r, a, m) {
    i['GoogleAnalyticsObject'] = r; i[r] = i[r] || function () {
        (i[r].q = i[r].q || []).push(arguments)
    }, i[r].l = 1 * new Date(); a = s.createElement(o),
    m = s.getElementsByTagName(o)[0]; a.async = 1; a.src = g; m.parentNode.insertBefore(a, m)
})(window, document, 'script', 'https://www.google-analytics.com/analytics.js', 'ga');

ga('create', 'UA-81225776-1', 'auto');
ga('send', 'pageview');


