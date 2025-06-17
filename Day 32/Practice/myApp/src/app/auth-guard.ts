import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, CanActivateChild, CanActivateFn, GuardResult, MaybeAsync, Router, RouterStateSnapshot } from '@angular/router';

@Injectable()
export class AuthGuard implements CanActivate,CanActivateChild{
  constructor(private router:Router){}
  
  canActivateChild(childRoute: ActivatedRouteSnapshot, state: RouterStateSnapshot): MaybeAsync<GuardResult> {
    const isAuthenticated = localStorage.getItem("token")?true:false;
    if(!isAuthenticated){
      this.router.navigate(['login']);
      return false;
    }
    return true;
  }
  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): MaybeAsync<GuardResult> {
    const isAuthenticated = localStorage.getItem("token")?true:false;
    if(!isAuthenticated){
      this.router.navigate(['login']);
      return false;
    }
    return true;
  }
  
}
