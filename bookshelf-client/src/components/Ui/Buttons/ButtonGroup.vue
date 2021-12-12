<template>
	<div class="button-group__container" @mouseleave="mouseLeaveHandler">
		<div :class="`button-group__header ${colorClass} ${className}`">
			<div
				v-if="variations[list]?.label"
				class="button-group__visible-label"
				title="Remove from list"
				@click="removeFromListsHandler"
			>
				<i class="fa fa-check"></i>
				<span>{{ variations[list]?.label }}</span>
				<i class="fa fa-times delete-icon"></i>
			</div>
			<div
				v-else
				class="button-group__visible-label"
				@click="() => addToListHandler('wantToRead')"
			>
				Want to read
			</div>
			<div class="button-group__handle" @click="setShowingDropdown(!showingDropdown)">
				<i v-if="!showingDropdown" class="fa fa-caret-down"></i>
				<i v-else class="fa fa-caret-up"></i>
			</div>
		</div>
		<div v-if="showingDropdown" :class="`button-group__dropdown ${colorClass}`">
			<div
				v-for="item in variations"
				:key="item.action"
				class="button-group__dropdown-item"
				@click="() => addToListHandler(item.action)"
			>
				<i v-if="list === item.action" class="fa fa-check"></i>
				{{ item.label }}
			</div>
		</div>
	</div>
</template>

<script>
	import { ref, computed } from "vue";

	const variations = {
		wantToRead: {
			action: "wantToRead",
			className: "green",
			label: "Want to read",
		},
		currentlyReading: {
			action: "currentlyReading",
			className: "yellow",
			label: "Currently reading",
		},
		read: {
			action: "read",
			className: "red",
			label: "Finished",
		},
	};

	export default {
		props: {
			list: String,
			className: String,
		},
		emits: ["add-to-list", "remove-from-list"],
		setup: (props, context) => {
			const showingDropdown = ref(false);

			const setShowingDropdown = (nextVal) => {
				showingDropdown.value = nextVal;
			};

			const addToListHandler = (listName) => {
				if (props.list === listName) {
					return;
				}

				context.emit("add-to-list", { previousList: props.list, nextList: listName });
				showingDropdown.value = false;
			};

			const removeFromListsHandler = () => {
				context.emit("remove-from-list", props.list);
			};

			const mouseLeaveHandler = () => {
				showingDropdown.value = false;
			};

			const colorClass = computed(() => {
				return variations[props.list]?.className ?? "default";
			});

			return {
				variations,
				showingDropdown,
				setShowingDropdown,
				mouseLeaveHandler,
				addToListHandler,
				removeFromListsHandler,
				colorClass,
			};
		},
	};
</script>

<style lang="scss" scoped>
	@import "./ButtonGroup";
</style>
