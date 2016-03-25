$(document).ready(function () {
    $("#removeRecord").click(function () {
        return confirm('Are you sure you want to delete this record?');
    });
});