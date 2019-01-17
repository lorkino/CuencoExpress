import { Component } from '@angular/core';
import { ExpressService } from '.././express.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {

  constructor(private accountService: ExpressService,
    private router: Router) { }


  isExpanded = false;

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  logout() {
    this.accountService.logout();

  }

  estaLogueado() {
    return this.accountService.estaLogueado();
  }

}
