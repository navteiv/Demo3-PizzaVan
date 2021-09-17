import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot } from "@angular/router";

@Injectable()
export class AuthGuard implements CanActivate{
  constructor(private router: Router){}

  canActivate(activeRoute: ActivatedRouteSnapshot, routeState: RouterStateSnapshot){
    if(localStorage.getItem("jwt")){
      return true;
    }
    else{
      this.router.navigate(["/login"]);
    }
    return false;
  }
}
