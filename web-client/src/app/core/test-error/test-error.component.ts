import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-test-error',
  templateUrl: './test-error.component.html',
  styleUrls: ['./test-error.component.scss'],
})
export class TestErrorComponent implements OnInit {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  ngOnInit(): void {}

  get400Error() {
    this.http.get(this.baseUrl + 'buggy/bad-request').subscribe(
      (response) => {
        console.log(response);
      },
      (error) => {
        console.log(error);
      }
    );
  }

  get401Error() {
    this.http.get(this.baseUrl + 'buggy/unauthorised').subscribe(
      (response) => {
        console.log(response);
      },
      (error) => {
        console.log(error);
      }
    );
  }

  get400ValidationError() {
    this.http.get(this.baseUrl + 'products/invalid-id').subscribe(
      (response) => {
        console.log(response);
      },
      (error) => {
        console.log(error);
      }
    );
  }

  get404Error() {
    this.http.get(this.baseUrl + 'products/00').subscribe(
      (response) => {
        console.log(response);
      },
      (error) => {
        console.log(error);
      }
    );
  }

  get500Error() {
    this.http.get(this.baseUrl + 'buggy/server-error').subscribe(
      (response) => {
        console.log(response);
      },
      (error) => {
        console.log(error);
      }
    );
  }
}
