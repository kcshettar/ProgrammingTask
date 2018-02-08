import { Component, Inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { FormBuilder } from '@angular/forms';
import { FormGroup } from '@angular/forms';
import { Validators } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';

// Service
import { WebServices } from './../../services/web.services';

@Component({
    selector: 'home',
    templateUrl: './home.component.html',
    // Error highlighter
    styles: ['.error{ background-color: #fff0f0 }']
})

export class HomeComponent {
    // Form name
    form: FormGroup;

    // Constructor
    constructor(private fb: FormBuilder, private webServices: WebServices) {
        this.form = fb.group({
            SignupName: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(50)]], // Validator property
            SignupEmail: ['', [Validators.required, emailValid()]], // Validator property
        })
    }

    // Form action - Regiter values to the service
    onSubmit() {
        console.log(this.form.errors); // Debug
        this.webServices.register(this.form.value);
        this.form.reset();
    }

    // Validation in home.component.html
    isValid(control: string) {
        return this.form.controls[control].invalid && this.form.controls[control].touched
    }
}

// Manual email validation method - using regular expression for the best result
function emailValid() {
    return (control: FormGroup) => {
        var regex = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
        return regex.test(control.value) ? null : { invalidEmail: true }
    }
}