// Call Stack : LIFO Context

function first()
{
    console.log("first");
    second();
}

function second()
{
    console.log("second");
}

first();

// ----------------------------------------------------------------------

// Hoisting

console.log(x);
var x = 5; 

var y;
console.log(y);
y=5;

// console.log(z);
// const z = 6;

// ----------------------------------------------------------------------

// Closures : Req data/ Surrounding state/lexical env
// aer functions that remember the surrounding data

function init()
{
    var name = "Mozilla"; // name local var created by init
    function displayname()
    {
        console.log(name); // use var declared in parent function
    }
    displayname();
}
init(); // Mozilla

// function init()
// {
//     var name = "Mozilla"; // name local var created by init
//     function displayname()
//     {
//         let name = "Google"
//         console.log(name); // use var declared in parent function
//     }
//     displayname();
// }
// init(); // Google

// let name = "Internet Explorer";
// function init()
// {
//     // var name = "Mozilla"; // name local var created by init
//     function displayname()
//     {
//         // let name = "Google"
//         console.log(name); // use var declared in parent function
//     }
//     displayname();
// }
// init(); // Internet Explorer


// Asynchrous code: Callbacks, Promises, Async/wait


console.log("I am learning JS");
setTimeout(() => {
    for(let i = 0; i < 5; i++)
    {
        console.log(i);
    }
},5000);
console.log("End of Loop");

// ------- Output -------

// 0
// 1
// 2
// 3
// 4

// ----------------------------------------------------------------------


// Callbacks

var arr = [1,2,3,4,5];
console.log(arr);

arr.forEach(function(element) {
    console.log(element);
})

const students = [
    { name: "ABC", subject: "Javascript"},
    { name: "XYZ", subject: "Java"}
]


// function enrollStudents(student) {
//     setTimeout(() => {
//         students.push(student);
//         console.log("Student has been enrolled.");
//     },3000)
// }

// function getStudents() {
//     setTimeout(() => {
//         let str = "";
//         students.forEach((student) => {
//             str += `<li> ${student.name} </li>`
//         })
//         document.getElementById("student-list").innerHTML = str;
//         console.log("Students detail has been fetched");
//     },1000);
// }

// let newStudent = {
//     name : "PQR",
//     subject: ".Net"
// }

// enrollStudents(newStudent);
// getStudents();


// ------- Output -------

// // Student Details
// // Students detail has been fetched
// // ABC
// // XYZ
// // Student has been enrolled.

function enrollStudents(student) {
    setTimeout(() => {
        students.push(student);
        console.log("Student has been enrolled.");
    },1000)
}

function getStudents() {
    setTimeout(() => {
        let str = "";
        students.forEach((student) => {
            str += `<li> ${student.name} </li>`
        })
        document.getElementById("student-list").innerHTML = str;
        console.log("Students detail has been fetched");
    },3000);
}

let newStudent = {
    name : "PQR",
    subject: ".Net"
}

enrollStudents(newStudent);
getStudents();

// ------- Output -------

// Student Details
// Student has been enrolled.
// ABC
// XYZ
// PQR
// Students detail has been fetched


// ----------------------------------------------------------------------


// callback Hell

// Promises: Object  => Resolve, Reject, Pending, 

function func1()
{
    return new Promise(function(resolve,reject){
        setTimeout(() => {
            const error = true;
            if(!error)
            {
                console.log("Your promise has been resolved");
                resolve();
            }
            else
            {
                console.log("Your promise has not been resolved");
                reject("Try again Later");
            }
        },2000)
    })
}
func1().then(() => {
        console.log("Thanks for resolving error");
    })
    .catch((error) => {
        console.log("OOps couldnt be resoved "+error);
    })

// ------- Output -------

// Your promise has not been resolved
// OOps couldnt be resoved Try again Later

