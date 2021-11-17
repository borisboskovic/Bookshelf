<template>
	<teleport to="#modal-container">
		<div class="backdrop" @click="dismissHandler">
			<div class="image-container" @click="imageClickHandler">
				<button class="close-btn" @click="dismissHandler">
					<i class="fa fa-times"></i>
				</button>
				<FallbackImage :source="image" />
			</div>
		</div>
	</teleport>
</template>

<script>
	import FallbackImage from "@/components/Ui/Imaging/FallbackImage.vue";

	export default {
		props: {
			image: String,
		},
		emits: ["dismiss"],
		components: {
			FallbackImage,
		},
		setup: (_, context) => {
			const dismissHandler = () => {
				context.emit("dismiss");
			};

			const imageClickHandler = (event) => {
				event.stopPropagation();
			};

			return {
				dismissHandler,
				imageClickHandler,
			};
		},
	};
</script>

<style lang="scss" scoped>
	@import "./PopupImage";
</style>
