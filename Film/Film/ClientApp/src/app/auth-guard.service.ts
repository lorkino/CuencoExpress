import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { ExpressService } from './express.service';
@Injectable({
  providedIn: 'root'
})
export class AuthGuardService implements CanActivate {

  constructor(private expressService: ExpressService,
    private router: Router) { }


  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    if (this.expressService.estaLogueado()) {
      return true;
    } else {
      this.router.navigate(['/login']);
      return false;
    }
  }
}
