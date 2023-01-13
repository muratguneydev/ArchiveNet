import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Art } from '../dto/Art';

@Component({
  selector: 'app-editable-art-view',
  templateUrl: './editable-art-view.component.html',
  styleUrls: ['./editable-art-view.component.css']
})
export class EditableArtViewComponent {

	@Input() art: Art = new Art;
	@Output() edit = new EventEmitter();

}
