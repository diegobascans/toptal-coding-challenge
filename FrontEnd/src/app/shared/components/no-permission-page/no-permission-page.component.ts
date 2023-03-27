import {Component} from "@angular/core";

@Component({
  selector: 'app-no-permission-page',
  template:
    `<div class="page-main">
        <div class="page-body">
            <h1>Not Authorized</h1>
        </div>
    </div>`,
  styleUrls: ['./no-permission-page.component.scss']
})
export class NoPermissionPageComponent {

}
