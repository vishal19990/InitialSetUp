import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, Subject } from 'rxjs';
import { map } from 'rxjs/operators';
import { AuthModel } from 'src/app/auth/models/login.model';
import { AppUrlConstants, LoginURLConstants } from 'src/app/shared/constants/url-constants';
import { NavPermission } from 'src/app/shared/models/role.model';
import { Router } from '@angular/router';

@Injectable({ providedIn: 'root' })
export class AuthenticationService {
    private currentUserSubject: BehaviorSubject<AuthModel>;
    public currentUser: Observable<AuthModel>;
    public permissions: BehaviorSubject<NavPermission[]> = new BehaviorSubject<NavPermission[]>([]);
    userClaims: BehaviorSubject<string[]> = new BehaviorSubject<string[]>([]);

    constructor(private http: HttpClient, private router: Router) {
        const user = JSON.parse(localStorage.getItem('currentUser'));
        this.currentUserSubject = new BehaviorSubject<AuthModel>(user);
        this.currentUser = this.currentUserSubject.asObservable();
    }

    login(username: string, password: string) {
        return this.http.post<AuthModel>(LoginURLConstants.LOGIN, { username, password })
            .pipe(map(user => {
                // store user details and jwt token in local storage to keep user logged in between page refreshes
                localStorage.setItem('currentUser', JSON.stringify(user));
                return user;
            }));
    }

    async getNavPermissions() {
        if (this.permissions.value.length == 0) {
            let result = await this.http.get<NavPermission[]>(`${AppUrlConstants.GET_NAV_PERMISSIONS_URL}`).toPromise();
            this.permissions.next(result);
        }
        return this.permissions.value;
    }

    setUserContext(user: AuthModel) {
        this.currentUserSubject.next(user);
    }

    public get currentUserValue(): AuthModel {
        return this.currentUserSubject.value;
    }

    logout() {
        // remove user from local storage to log user out
        localStorage.removeItem('currentUser');
        this.currentUserSubject.next(null);
        this.router.navigate(['/login'])
    }
}
