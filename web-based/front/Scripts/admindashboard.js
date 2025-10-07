// count current status
function updateViolationCounts() {
    let rows = document.querySelectorAll('#tableBody tr');
    let unsettledCount = 0;
    let settledCount = 0;

    rows.forEach(row => {
        // Check the status of each violation and increment the counts
        let statusButton = row.querySelector('.status-btn');
        if (statusButton && statusButton.textContent.trim() === 'Unsettled') {
            unsettledCount++;
        } else if (statusButton && statusButton.textContent.trim() === 'Settled') {
            settledCount++;
        }
    });

    // Update the displayed counts
    document.getElementById('unsettledCount').textContent = unsettledCount;
    document.getElementById('settledCount').textContent = settledCount;
}

// search function
document.getElementById('searchInput').addEventListener('input', function () {
    let filter = this.value.toLowerCase();
    let rows = document.querySelectorAll('#tableBody tr');

    // If the input is empty, show all rows
    if (filter === '') {
        rows.forEach(row => {
            row.style.display = ''; // Show all rows
        });
    } else {
        rows.forEach(row => {
            let studentId = row.cells[2].textContent.trim().toLowerCase(); // Student I.D
            let studentName = row.cells[3].textContent.trim().toLowerCase(); // Student Name
            let lastNameFirstTwoChars = studentName.split(' ')[0].slice(0, 2).toLowerCase(); // First 2 chars of Last Name

            // Show or hide rows based on the first two characters of the search input
            if (studentId.slice(0, 2) === filter.slice(0, 2) || lastNameFirstTwoChars === filter.slice(0, 2)) {
                row.style.display = ''; // Show row
            } else {
                row.style.display = 'none'; // Hide row
            }
        });
    }
});
// Initial update of violation counts
updateViolationCounts();

// popover
document.addEventListener('DOMContentLoaded', function () {

    // Initialize all popovers
    let popoverTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="popover"]'));
    popoverTriggerList.forEach(function (popoverTriggerEl) {
        new bootstrap.Popover(popoverTriggerEl);
    });

    // Apply styling to status buttons
    const statusButtons = document.querySelectorAll('.status-btn');
    statusButtons.forEach(button => {
        if (button.textContent.trim() === 'Settled') {
            button.classList.add('btn-success');
        } else if (button.textContent.trim() === 'Unsettled') {
            button.classList.add('btn-danger');
        }
    });

    // Separate functionality: Disable "Settled" buttons
    disableSettledButtons();

    function disableSettledButtons() {
        statusButtons.forEach(button => {
            if (button.textContent.trim() === 'Settled') {
                button.disabled = true; // Disable the button
            }
        });
    }
});

