import { Component, HostListener } from '@angular/core';
import { ExpressService } from '../.././express.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {

  constructor(private accountService: ExpressService,
    private router: Router) { }

  public show: boolean = false;
  private wasInside = false;

  isExpanded = false;
  changeShow() {
    this.show = !this.show;
  }
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

  @HostListener('click')
  clickInside() {
    this.wasInside = true;
  }

  @HostListener('document:click', ['$event'])
  clickout() {  
    if (!this.wasInside) {
      this.show = false;
    }
    this.wasInside = false;
  }

}
