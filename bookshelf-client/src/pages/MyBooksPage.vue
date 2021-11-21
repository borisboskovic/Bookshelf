<template>
	<div :style="{ width: '200px', display: 'inline-block', margin: '5px' }">
		<ButtonGroup variant="" />
	</div>
	<div :style="{ width: '200px', display: 'inline-block', margin: '5px' }">
		<ButtonGroup variant="green" />
	</div>
	<div :style="{ width: '200px', display: 'inline-block', margin: '5px' }">
		<ButtonGroup variant="yellow" />
	</div>
	<div :style="{ width: '200px', display: 'inline-block', margin: '5px' }">
		<ButtonGroup variant="red" />
	</div>
</template>

<script>
	import { onMounted } from "vue";
	import ButtonGroup from "@/components/Ui/Buttons/ButtonGroup.vue";

	export default {
		components: {
			ButtonGroup,
		},
		setup: () => {
			onMounted(() => {
				document.title = "BookSelf - My Books";
			});
		},
	};
</script>

<style lang="scss" scoped></style>
