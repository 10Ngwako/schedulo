const form = document.getElementById('assignmentForm');
const list = document.getElementById('assignmentsList');

function getAssignments() {
    return JSON.parse(localStorage.getItem('assignments') || '[]');
}

function saveAssignments(assignments) {
    localStorage.setItem('assignments', JSON.stringify(assignments));
}

function renderAssignments() {
    const assignments = getAssignments();
    list.innerHTML = '';

    assignments.forEach(a => {
        const li = document.createElement('li');
        li.className = 'list-group-item d-flex justify-content-between align-items-center';
        li.dataset.id = a.id;
        li.innerHTML = `
            <div>
                <strong>${a.title}</strong> • ${a.courseCode} • Due ${new Date(a.dueDate).toLocaleDateString()}
            </div>
            <button class="btn btn-sm btn-danger delete-btn">Delete</button>
        `;
        list.appendChild(li);
    });
}

// Handle form submission
form.addEventListener('submit', e => {
    e.preventDefault();

    const assignments = getAssignments();
    const newAssignment = {
        id: Date.now(),
        title: document.getElementById('title').value,
        courseCode: document.getElementById('courseId').value,
        dueDate: document.getElementById('dueDate').value,
        priority: document.getElementById('priority').value
    };

    assignments.push(newAssignment);
    saveAssignments(assignments);
    renderAssignments();

    form.reset();
});

// Handle delete
list.addEventListener('click', e => {
    if (e.target.classList.contains('delete-btn')) {
        const id = parseInt(e.target.closest('li').dataset.id);
        let assignments = getAssignments();
        assignments = assignments.filter(a => a.id !== id);
        saveAssignments(assignments);
        renderAssignments();
    }
});

// Initial render
renderAssignments();
