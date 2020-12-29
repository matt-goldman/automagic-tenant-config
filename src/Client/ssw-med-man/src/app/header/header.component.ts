import { Component, OnInit,Input } from '@angular/core';
import { UserService } from '../services/user.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Subscription } from 'rxjs';
import { Router } from '@angular/router';
import { ConfigClient } from '../../helpers/api-client';
import { MatDialog } from '@angular/material';
import { QrDialogComponent } from '../qr-dialog/qr-dialog.component';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {

  constructor(userService: UserService, private _snackBar: MatSnackBar, private router: Router, private configClient: ConfigClient, public dialog: MatDialog) {
    this.userService = userService;
   }

  @Input()
  loggedIn: boolean;
  subscription: Subscription;
  userService: UserService;

  ngOnInit() {
    this.subscription = this.userService.authNavStatus$.subscribe(loggedin => this.loggedIn = loggedin);
  }

  logout(){
    this.userService.logout();
    this.loggedIn = false;
    this._snackBar.open("You have been logged out", "OK" ,{ duration: 3000} );
    this.router.navigate(['/home']);
  }

  showQRCode() {
    this.configClient.get().subscribe(result => {
      var config = JSON.stringify(result);
      console.log(config);
      const dialogRef = this.dialog.open(QrDialogComponent, {
        width: '600px',
        data: btoa(config)
      });
    },
    err =>{
      this._snackBar.open(err, "OK", {duration: 3000});
    });
  }

}
