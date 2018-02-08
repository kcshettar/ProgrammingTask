import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import { Router } from '@angular/router';

@Injectable()
export class WebServices {

    BASE_URL = 'http://localhost:61601/api/User';
    constructor(private http: Http, private router: Router) { }

    register(user: any) {
        
        this.http.post(this.BASE_URL + '/register', user).subscribe(
            res => {
                this.router.navigate(['/']);
            });
    }
}