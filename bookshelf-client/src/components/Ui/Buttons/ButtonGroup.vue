<template>
	<div class="button-group__container" @mouseleave="mouseLeaveHandler">
		<div :class="`button-group__header ${colorClass}`">
			<div class="button-group__visible-label">Selected</div>
			<div class="button-group__handle" @click="setShowingDropdown(!showingDropdown)">
				<i v-if="!showingDropdown" class="fa fa-caret-down"></i>
				<i v-else class="fa fa-caret-up"></i>
			</div>
		</div>
		<div v-if="showingDropdown" :class="`button-group__dropdown ${colorClass}`">
			<div class="button-group__dropdown-item">Want to read</div>
			<div class="button-group__dropdown-item">Currently reading</div>
			<div class="button-group__dropdown-item">Read</div>
		</div>
	</div>
</template>

<script>
	import { ref, computed } from "vue";

	export default {
		props: {
			variant: {
				type: String,
				required: false,
				validator: (value) => {
					return ["green", "yellow", "red", "default"].indexOf(value) !== -1;
				},
			},
		},
		setup: (props) => {
			const showingDropdown = ref(false);

			const setShowingDropdown = (nextVal) => {
				showingDropdown.value = nextVal;
			};

			const mouseLeaveHandler = () => {
				showingDropdown.value = false;
			};

			const colorClass = computed(() => {
				if (["green", "yellow", "red"].indexOf(props.variant) != -1) {
					return props.variant;
				} else {
					return "default";
				}
			});

			return {
				showingDropdown,
				setShowingDropdown,
				mouseLeaveHandler,
				colorClass,
			};
		},
	};
</script>

<style lang="scss" scoped>
	@import "./ButtonGroup";
</style>
