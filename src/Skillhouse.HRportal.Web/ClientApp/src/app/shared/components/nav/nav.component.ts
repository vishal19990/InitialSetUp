import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from 'src/app/core/services/authentication.service';
import { INavItem } from '../../models/nav.model';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.scss']
})
export class NavComponent implements OnInit {
  navItems: INavItem[] = NAVITEMS;
  constructor(private authService: AuthenticationService) { }

  ngOnInit(): void {
    this.initNavigation();
  }

  initNavigation() {
    this.authService.permissions.subscribe((permissions) => {
      this.navItems.forEach((nav) => {
        let modulePermission = permissions.find(
          (x) => x.code == nav.routePermissionCode
        );
        if (modulePermission) {
            nav.visible = true;
          } else {
            nav.visible = false;
          }
      });
    });
  }

  get allowedNavigations() {
    return this.navItems.filter((x) => x.visible);
  }
}

export const NAVITEMS: INavItem[] = [
  {
    title: 'Dashboard',
    routeLink: '/home',
    routePermissionCode: 'DashboardModule',
    visible: false,
    icon: 'discipline-icon',
  },
  {
    title: 'Lead management',
    routeLink: '/lead',
    routePermissionCode: 'LeadsManagementModule',
    visible: false,
    icon: 'discipline-icon',
  },
  {
    title: 'Activities',
    routeLink: '/activities',
    routePermissionCode: 'ActivitiesModule',
    visible: false,
    icon: 'discipline-icon',
  },
  {
    title: 'Student Management',
    routeLink: '/student',
    routePermissionCode: 'StudentsManagementModule',
    visible: false,
    icon: 'discipline-icon',
  },
  {
    title: 'Marketing',
    routeLink: '/marketing',
    routePermissionCode: 'MarketingModule',
    visible: false,
    icon: 'discipline-icon',
  },
  {
    title: 'Reports',
    routeLink: '/reports',
    routePermissionCode: 'ReportsModule',
    visible: false,
    icon: 'discipline-icon',
  },
  {
    title: 'Courses',
    routeLink: '/course',
    routePermissionCode: 'CourseManagementModule',
    visible: false,
    icon: 'discipline-icon',
  },
  {
    title: 'Users',
    routeLink: '/user',
    routePermissionCode: 'UserManagementModule',
    visible: false,
    icon: 'discipline-icon',
  },
  {
    title: 'Settings',
    routeLink: '/settings',
    routePermissionCode: 'SettingsModule',
    visible: false,
    icon: 'discipline-icon',
  }
];
