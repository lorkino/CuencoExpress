import { Component, OnInit, Input } from '@angular/core';
import { ExpressService } from '../.././express.service';
import { HttpClientModule } from '@angular/common/http';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { FormGroup } from '@angular/forms';
declare var jquery: any;
declare var $: any;
@Component({
  selector: 'app-knowledges',
  templateUrl: './knowledges.component.html',
  styleUrls: ['./knowledges.component.css']
})
export class KnowledgesComponent implements OnInit {
  @Input() group: FormGroup;
  constructor(private accountService: ExpressService, private http: HttpClient) { }

  ngOnInit() {

    $(function () {
      $('#select-name').selectize({
        plugins: ['remove_button'],
        onChange: function (value) {
          (<any>document.getElementById("knowledges")).value = "";
          (<any>document.getElementById("explanation")).value = "";
            console.log(value);
            for (var i = 0; i < document.getElementById("select-name").childNodes.length; i++) {
              (<any>document.getElementById("knowledges")).value += ((<any>document.getElementById("knowledges")).value != '' ? ' - ' : '') + (<any>document.getElementById("select-name")).childNodes[i].innerHTML;
              (<any>document.getElementById("explanation")).value += ((<any>document.getElementById("explanation")).value != '' ? ' - ' : '') + (<any>document.getElementById("select-name")).childNodes[i].value;
            }
            console.log((<any>document.getElementById("knowledges")).value);
        },
        onFocus: function (value) {
          console.log(value);
        }
      });

      $('#contacts').submit(function (e) {
        e.preventDefault();
        alert('You have chosen the following users: ' + JSON.stringify($('#select-name').val()));
      });
    });
  }


  searchKnowlegment(event: any) {
  

 var response = this.http.get<any>("https://ms-autocomplete.spain.schibsted.io/skills/" + event.target.value);
    response.subscribe(function (response) {
      console.log(response);
      Array.from(response).forEach(function (element: any) {  
        $(function () {
          var $select = $('#select-name').selectize();
          element.name = element.name.replace("<strong>", "");
          element.name = element.name.replace("</strong>", "");
          var selectize = $select[0].selectize;
          selectize.addOption({ value: element.id, text: element.name });
          selectize.refreshOptions();
        });
      });

    },
      error => console.log(error));
  }

}
