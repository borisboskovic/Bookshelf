<template>
	<div class="image-uploader__outside-container">
		<label :for="name" class="image-uploader__label">{{ label }}</label>
		<div
			class="image-uploader__image-container"
			@click="uploaderClickHandler"
			@dragenter="dragEnterHandler"
			@dragleave="dragLeaveHandler"
			@dragover="dragOverHandler"
			@drop="dropHandler"
		>
			<div v-if="hasDragOver" class="drag-border" />
			<img v-if="loadedImage" :src="loadedImage" alt="New profile photo" />
			<div v-else class="image-uploader__placeholder">
				<i class="fa fa-image"></i>
				<div>Upload image</div>
			</div>
			<input
				type="file"
				:name="name"
				accept="image/*"
				ref="fileInputRef"
				@change="handleInputChange"
				hidden
			/>
		</div>
		<div class="error-container">
			<ErrorMessage :name="name" />
		</div>
	</div>
</template>

<script>
	import { ref } from "vue";
	import { useField, ErrorMessage } from "vee-validate";

	export default {
		props: {
			name: String,
			label: String,
		},
		components: {
			ErrorMessage,
		},
		setup: (props, context) => {
			const fileInputRef = ref(null);
			const loadedImage = ref();
			const hasDragOver = ref(false);
			const { setValue } = useField(props.name);

			const uploaderClickHandler = () => {
				fileInputRef.value.click();
			};

			const dragEnterHandler = () => {
				hasDragOver.value = true;
			};
			const dragLeaveHandler = () => {
				hasDragOver.value = false;
			};
			const dragOverHandler = (event) => {
				event.preventDefault();
			};

			const readFromFile = (file) => {
				const reader = new FileReader();
				reader.onloadend = () => {
					loadedImage.value = reader.result;
				};
				if (/image\/*/.test(file.type)) {
					reader.readAsDataURL(file);
					setValue(file);
				} else {
					context.emit("wrongformat");
				}
			};

			const dropHandler = (event) => {
				event.preventDefault();
				hasDragOver.value = false;
				readFromFile(event.dataTransfer.files[0]);
			};

			const handleInputChange = () => {
				readFromFile(fileInputRef.value.files[0]);
			};

			return {
				fileInputRef,
				loadedImage,
				uploaderClickHandler,
				handleInputChange,
				hasDragOver,
				dragEnterHandler,
				dragLeaveHandler,
				dragOverHandler,
				dropHandler,
			};
		},
	};
</script>

<style lang="scss" scoped>
	@import "./ImageUploader";
</style>
