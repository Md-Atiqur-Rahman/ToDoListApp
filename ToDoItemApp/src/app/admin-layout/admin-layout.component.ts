import {
  Component,
  OnInit,
  ChangeDetectorRef,
  ViewChild,
  ElementRef,
} from '@angular/core';
import { MediaMatcher } from '@angular/cdk/layout';

import { Router } from '@angular/router';
import { Menu } from '../models/menu.model';
import { NavItem } from '../models/navitem.model';
import { JwtHelperService } from '@auth0/angular-jwt';

@Component({
  selector: 'app-admin-layout',
  templateUrl: './admin-layout.component.html',
  styleUrls: ['./admin-layout.component.scss']
})
export class AdminLayoutComponent implements OnInit {
  routerLink = '';
  _menu?: Menu[];
  mobileQuery: MediaQueryList;
  fillerNav = Array(5)
    .fill(0)
    .map((_, i) => `Nav Item ${i + 1}`);
  fillerContent = Array(2)
    .fill(0)
    .map(
      () =>
        `Open side nav, and click on any navigation to close the opened side nav.`
    );
  private _mobileQueryListener: () => void;

  constructor(
    changeDetectorRef: ChangeDetectorRef,
    media: MediaMatcher,
    private _router: Router,
    private jwtHelper: JwtHelperService) { 
      this.mobileQuery = media.matchMedia('(max-width: 600px)');
    this._mobileQueryListener = () => changeDetectorRef.detectChanges();
    this.mobileQuery.addListener(this._mobileQueryListener); 
  }

  ngOnInit() {
    this.loadMenu();
    this.isUserAuthenticated();
  }
  ngOnDestroy(): void {
    this.mobileQuery.removeListener(this._mobileQueryListener);
  }
  
  loadMenu() {
    return (this._menu = [
      { MenuName: 'Employee ', path: '/config/employee' },
      { MenuName: 'Register', path: '/register' },
      { MenuName: 'Dashboard', path: '/dashboard' },
      { MenuName: 'Login', path: '/login' },
    ]);
  }
  // @ViewChild('appDrawer')
  // appDrawer!: ElementRef;
  navItems: NavItem[] = [
    {
      displayName: 'Config',
      iconName: 'recent_actors',
      route: 'devfestfl',
      children: [
        {
          displayName: 'To Do Items',
          iconName: 'person',
          route: 'config/todoItems/todoForm',
        }
      ],
    },
    {
      displayName: 'Dashboard',
      iconName: 'videocam',
      route: 'dashboard',
    },
  ];

  userName:string;
  
  isLoggedIn=false;
  isUserAuthenticated() {
    
    
    const token: any = localStorage.getItem("jwt");
     this.userName = localStorage.getItem("loginUser");
    console.log(token,'token');
    if (token && !this.jwtHelper.isTokenExpired(token)) {
      this.isLoggedIn=true;
      return true;
    } else {
      this.isLoggedIn=false;
      return false;
    }
  }

  LogOut() {
    localStorage.removeItem("jwt");
    localStorage.removeItem("loginUser");
    this._router.navigate(['/login']);
  }
}
