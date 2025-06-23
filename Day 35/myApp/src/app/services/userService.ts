import { BehaviorSubject, Observable } from "rxjs"
import { UserLoginModel } from "../models/userLoginModel";
import { HttpHeaders, HttpClient } from "@angular/common/http";
import { inject } from "@angular/core";

export class userService {
    private usernameSubject =new BehaviorSubject<string|null>(null);
    username$:Observable<string|null> = this.usernameSubject.asObservable();
    private http = inject(HttpClient);

    validateUserLogin(user: UserLoginModel)
    {
        if(user.username.length<3)
        {
            this.usernameSubject.next(null);
            this.usernameSubject.error("Too short for username");
        }
            
        else
        {
            this.callLoginAPI(user).subscribe(
            {
                next:(data:any)=>{
                    this.usernameSubject.next(user.username);
                    localStorage.setItem("token",data.accessToken)
                }
            }
            )
        }
    }

    callGetProfile()
    {
        var token = localStorage.getItem("token")
        const httpHeader = new HttpHeaders({
            'Authorization':`Bearer ${token}`
        })
        console.log("INto callgetProfile");
        return this.http.get('https://dummyjson.com/auth/me',{headers:httpHeader});
        
    }

    callLoginAPI(user:UserLoginModel)
    {
        return this.http.post("https://dummyjson.com/auth/login",user);
    }

    logout()
    {
        this.usernameSubject.next(null);
    }
}