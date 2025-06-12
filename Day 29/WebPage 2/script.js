const students = [
    { name: "ABC", subject: "JavaScript" },
    { name: "XYZ", subject: "Java" },
];

// ----------- Callback Version -----------
function fetchStudentsCallback(callback) {
    setTimeout(() => {
        console.log("Waits 1 second and call callback function by passing students list");
        callback(students);
    }, 1000);
}

function getUsersCallback() {
    console.log("Pressed Get Users (Callback), inside getUsersCallback");
    fetchStudentsCallback((studentsList) => {
        renderStudents(studentsList);
        console.log("Fetched using callback");
    });
}

// ----------- Promise Version -----------
function fetchStudentsPromise() {
    return new Promise((resolve) => {
        setTimeout(() => {
            resolve(students);
        }, 1000);
    });
}

// function fetchStudentsPromise() {
//     return new Promise((reject) => {
//         setTimeout(() => {
//             reject("Try again");
//         }, 1000);
//     });
// }

function getUsersPromise() {
    console.log("Pressed Get Users (Promise), inside getUsersPromise");
    fetchStudentsPromise()
        .then((studentsList) => {
            renderStudents(studentsList);
            console.log("Fetched using promise");
        })
        .catch((err) => console.error("Error:", err));
}

// ----------- Async/Await Version -----------
async function fetchStudentsAsync() {
    return new Promise((resolve) => {
        setTimeout(() => {
            resolve(students);
        }, 1000);
    });
}

async function getUsersAsyncAwait() {
    try {
        console.log("Pressed Get Users (Async / Await), inside getUsersAsyncAwait");
        const studentsList = await fetchStudentsAsync();
        renderStudents(studentsList);
        console.log("Fetched using async/await");
    } catch (error) {
        console.error("Error:", error);
    }
}

// ----------- Render into the html file -----------
function renderStudents(studentsList) {
    const listElement = document.getElementById("student-list");
    listElement.innerHTML = ""; 
    studentsList.forEach(student => {
        const li = document.createElement("li");
        li.textContent = `${student.name} - ${student.subject}`;
        listElement.appendChild(li);
    });
}
