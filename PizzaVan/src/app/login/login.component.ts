import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthenticateService } from '../core/services/authenticate.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  loginForm!: FormGroup;
  constructor(private fb: FormBuilder, private authen: AuthenticateService, private router: Router ) {
    this.loginForm = this.fb.group({
      username: ['', [Validators.required]],
      password: ['', [Validators.required]]
    });
  }

  ngOnInit(): void {
  }
  onSubmit(): void{
    console.log(this.loginForm.value.username + " "+ this.loginForm.value.password)
    let username = this.loginForm.value.username;
    let password = this.loginForm.value.password;
    this.authen.login(username, password);
  }
}
