import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';

@Component({
    selector: 'fetchdata',
    templateUrl: './fetchdata.component.html'
})
export class FetchDataComponent {
    public userNew: UserRetrievalFrontEnd[];

    constructor(http: Http, @Inject('BASE_URL') baseUrl: string) {
        http.get(baseUrl + 'api/SampleData/SignedUpUser').subscribe(result => {
            this.userNew = result.json() as UserRetrievalFrontEnd[];
        }, error => console.error(error));
    }
}

interface UserRetrievalFrontEnd {
    id: string;
    name: number;
    email: number;
    dateTime: string;
}