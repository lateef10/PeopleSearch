import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators} from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import {NewPersonService} from './shared/new-person.service';

declare var $:any;

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  providers: [NewPersonService]
})
export class AppComponent {
  title = 'PeopleSearchApp';
  ImageToUpload : File = null;
  imageUrl: string = "/assets/Images/imageblank.png";

  readonly endpoint = "http://localhost:60499/";
  DataHolder: any = []; 
  public show:boolean = false;
  search: string;
  errors: string;
  constructor(private http: HttpClient, private personService: NewPersonService) { }
  toggle() {
    this.show = !this.show;
  }
  getPeople(){
    return this.http.get(this.endpoint + "api/Home/")
    .subscribe(data =>{
      this.DataHolder = data;
    },
    error => {
      this.errors = "Error occured!";
    });
  }
  getSomePeople(){
    this.toggle();
    return this.http.get(this.endpoint + "api/Home?search=" + this.search)
    .subscribe(data =>{
      this.DataHolder = data; 
      this.toggle();
      },
      error => {
        this.errors = "Error occured!";
      }
      );
  }
  
 handleFileInput(file: FileList)
 {
    this.ImageToUpload = file.item(0);

    //show image preview
    var reader = new FileReader();
    reader.onload = (event:any) =>{
      this.imageUrl = event.target.result;
    }
    reader.readAsDataURL(this.ImageToUpload);
 }

 OnSubmit(FirstName, LastName, Address, Age, Interests, Picture){
    this.personService.PostPerson(FirstName.value, LastName.value, Address.value, Age.value, Interests.value, this.ImageToUpload)
    .subscribe(data =>{
      FirstName.value = null;
      LastName.value = null;
      Address.value = null;
      Age.value = null;
      Interests.value = null;
      Picture.value = null;
      this.imageUrl = "/assets/Images/imageblank.png";

      alert("Info Added successfully");
      this.getPeople();
      },
      error => {
        alert("Error occured!");
      });
 }

  ngOnInit() {
    this.getPeople();
    }
    

}