jQuery(document).ready(function () {
	$('.dobpicker').datepicker({
		autoclose: true
	});

	$("#userfiles").fileinput({
		required: false,
		browseClass: "btn btn-secondary",
		browseIcon: "",
		removeClass: "btn btn-danger",
		removeLabel: "",
		removeIcon: "<i class='icon-trash-alt1'></i>",
		showUpload: false
	});
})