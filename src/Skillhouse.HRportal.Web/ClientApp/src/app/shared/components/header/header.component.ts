import { Component, ElementRef, HostListener, OnInit, Renderer2, ViewChild } from '@angular/core';
import { AuthenticationService } from 'src/app/core/services/authentication.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {
  @ViewChild('menu') menu: ElementRef;
  @ViewChild('menuTrigger') menuTrigger: ElementRef;

  isProfileMenuOpen = false;
  constructor(private auth: AuthenticationService,private renderer: Renderer2) { 
    this.renderer.listen('window', 'click',(e:Event)=>{
      if(e.target!==this.menu?.nativeElement && e.target!==this.menuTrigger?.nativeElement){
              this.isProfileMenuOpen=false;
          }
      });
  }

 
  ngOnInit(): void {

  }
 
  toggleMenu() {
    this.isProfileMenuOpen = !this.isProfileMenuOpen;
  }

  @HostListener('window:click', ['$event'])
  closeMenu(event: MouseEvent) {
}

  logout() {
    this.auth.logout();
  }
}
