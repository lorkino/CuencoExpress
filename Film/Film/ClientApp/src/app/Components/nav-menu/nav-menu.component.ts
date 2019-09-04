import { Component, HostListener } from '@angular/core';
import { ExpressService } from '../.././express.service';
import { Router } from '@angular/router';
import { asEnumerable } from 'linq-es2015';
import { User } from '../../Models/user';
import { CommonService } from '../../services/common/common.service';
@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  user: User;
  countJobs: number;
  public show: boolean = false;
  private wasInside = false;

  constructor(private accountService: ExpressService,
    private router: Router, private commonService: CommonService) {   
    
    this.commonService.UserSubject.subscribe(
      () => {
        this.updateBadgets();
      }
    );
    
    
  }
  ngOnInit() {
    //demo service returns array of string
    this.user = this.accountService.getUser();
    this.updateBadgets();
  }
  
  updateBadgets() {
    this.user = this.accountService.getUser();
    if (this.user)
      //@ts-ignore
      this.countJobs = asEnumerable(this.user.notifications).Where(a => a.type == 0).Count();
    console.log("upBadget");
    console.log(this.countJobs);
  }

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
    this.user=null;
    this.countJobs=0;
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
