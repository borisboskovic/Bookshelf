<template>
	<div class="progress-modal-container" @click="modalClickHandler">
		<div class="input-row">
			<span>Currently on:</span>
			&nbsp;
			<input
				ref="inputRef"
				type="number"
				min="0"
				:max="totalPages"
				v-model="enteredValue"
				@keyup="keyboardSubmitHandler"
			/>
			&nbsp;
			<span>of</span>
			&nbsp;
			<input type="number" readonly disabled :value="totalPages" />
		</div>
		<div class="finished-row">
			Or
			<span @click="finishHandler">I'm finished</span>
		</div>
		<div class="button-row">
			<span @click="dismissHandler">Cancel</span>
			<ButtonComponent size="small" :disabled="!allowUpdate" @click="updateHandler"
				>Update</ButtonComponent
			>
		</div>
	</div>
</template>

<script>
	import { ref, computed, onMounted } from "vue";
	import ButtonComponent from "@/components/Ui/Buttons/ButtonComponent.vue";

	export default {
		props: {
			totalPages: Number,
			pagesRead: Number,
		},
		emits: ["dismiss", "update-progress", "finish-book"],
		components: {
			ButtonComponent,
		},
		setup: (props, context) => {
			const enteredValue = ref(props.pagesRead);
			const inputRef = ref();

			const allowUpdate = computed(() => {
				return (
					enteredValue.value !== props.pagesRead &&
					enteredValue.value >= 0 &&
					enteredValue.value <= props.totalPages &&
					enteredValue.value !== ""
				);
			});

			onMounted(() => {
				inputRef.value.select();
			});

			const modalClickHandler = (event) => {
				event.stopPropagation();
			};

			const dismissHandler = () => {
				context.emit("dismiss");
			};

			const updateHandler = () => {
				context.emit("update-progress", enteredValue.value);
			};

			const finishHandler = () => {
				context.emit("finish-book");
			};

			const keyboardSubmitHandler = (event) => {
				if (event.key === "Enter" && allowUpdate.value) {
					context.emit("update-progress", enteredValue.value);
				}
			};

			return {
				enteredValue,
				allowUpdate,
				inputRef,
				modalClickHandler,
				dismissHandler,
				updateHandler,
				keyboardSubmitHandler,
				finishHandler,
			};
		},
	};
</script>

<style lang="scss" scoped>
	@import "./UpdateProgressModal";
</style>
