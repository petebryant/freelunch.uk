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


