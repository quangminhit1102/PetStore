const name =  document.getElementById("userFullName");
const birthday =  document.getElementById("userBirthday");
const phone =  document.getElementById("userPhone");
const email =  document.getElementById("userEmail");
const address =  document.getElementById("userAddress");
const user_save = document.getElementById("user_save");
const Gender = document.getElementById("Gender");
function handleOnChange(){
    name.disabled = false;
    birthday.disabled = false;
    phone.disabled = false;
    email.disabled = false;
    address.disabled = false;
    Gender.disabled = false;
    user_save.style.display = "block";
}
function handleOffChange(){
    location.reload();
}