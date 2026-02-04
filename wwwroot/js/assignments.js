const form = document.getElementById('assignmentForm');
const list = document.getElementById('assignmentsList');

form.addEventListener('submit', async e => {
    e.preventDefault();

    const data = {
        title: document.getElementById('title').value,
        courseId: parseInt(document.getElementById('courseId').value),
        dueDate: document.getElementById('dueDate').value,
        priority: document.getElementById('priority').value,
        status: 'Pending'
    };

    const res = await fetch('/api/assignments', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(data)
    });

    if (res.ok) location.reload();
});

list.addEventListener('click', async e => {
    if (e.target.classList.contains('delete-btn')) {
        const li = e.target.closest('li');
        const id = li.dataset.id;

        const res = await fetch(`/api/assignments/${id}`, { method: 'DELETE' });
        if (res.ok) li.remove();
    }
});
