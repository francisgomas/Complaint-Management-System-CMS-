jQuery(document).ready(function () {
	$('.dobpicker').datepicker({
		autoclose: true,
	});

	$("#jobs-application-resume").fileinput({
		required: true,
		browseClass: "btn btn-secondary",
		browseIcon: "",
		removeClass: "btn btn-danger",
		removeLabel: "",
		removeIcon: "<i class='icon-trash-alt1'></i>",
		showUpload: false
	});
})