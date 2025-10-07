// Ensure that the popover functionality is initialized
document.addEventListener('DOMContentLoaded', function () {
    // Initialize all popovers on page load
    var popoverTriggerList = document.querySelectorAll('[data-bs-toggle="popover"]');
    var popoverList = [...popoverTriggerList].map(function (popoverTriggerEl) {
        return new bootstrap.Popover(popoverTriggerEl);
    });
});

$('.violation-checkbox').on('change', function () {
    // Create a list item with the violation definition
    let violationText = $(this).data('violation');
    let violationsList = $('#violationsList');

    if ($(this).prop('checked')) {
        // Add violation to the list if checked
        violationsList.append('<li class="list-group-item">' + violationText + '</li>');
    } else {
        // Remove violation from the list if unchecked
        violationsList.find('li').filter(function () {
            return $(this).text() === violationText;
        }).remove();
    }
});