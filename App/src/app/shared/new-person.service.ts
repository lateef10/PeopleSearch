import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
@Injectable()
export class NewPersonService {

  constructor(private http: HttpClient) { }

  readonly endpoint = "http://localhost:60499/";
  PostPerson(FirstName: string, LastName: string, Address: string, Age: number, Interests: string, ImageToUpload: File){
    const formData: FormData = new FormData();

    formData.append('FirstName', FirstName);
    formData.append('LastName', LastName);
    formData.append('Address', Address);
    formData.append('Age', Age.toString());
    formData.append('Interests', Interests);
    formData.append('Picture', ImageToUpload);
    
    return this.http.post(this.endpoint + 'api/Home', formData);
  }
}
