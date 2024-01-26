.data
n qword ?
it qword ?

.code
asmFloydWarshall proc
	mov n, rdx      ;move vertex from register to variable
	mov it, rbx     ;move iteration of loop to variable

    xor     rbx, rbx ; rbx is loop iterator
    xor     r8, r8   ; r8 is increasing start every time of 8 int numbers if vertex number is longer than 8


inner_loop_start:
    ; Compute indices
    mov     r10, it              ; r10 = iteration
    imul    r10, n               ; r10 = iteration * n
    add     r10, r8              ; r10 = iteration * n + next set if graph vertices is longer than 8
    shl     r10, 2

    mov     r11, rbx              ; r11 = i
    imul    r11, n                ; r11 = i * n
    add     r11, it               ; r11 = i * n + iteration
    shl     r11, 2

    mov     r12, rbx               ; r12 = i
    imul    r12, n                 ; r12 = i * n
    add     r12, r8                ; r12 = i * n + next set if graph vertices is longer than 8
    shl     r12, 2

    vmovdqu  ymm11, ymmword ptr [rcx + r10] ;Load resultGraph[iteration * n + next set vector]

    vmovdqu  ymm12, ymmword ptr [rcx + r12] ;Load resultGraph[i * n + next set vector]

    mov r13, [rcx + r11]    ;Load resultGraph[i * n + iteration]
    movq xmm10, r13         

    vpbroadcastd ymm10, xmm10   ;duplicate resultGraph[i * n + iteration]
    vpaddd ymm13, ymm10, ymm11  ;Add vector resultGraph[i * n + iteration] + resultGraph[iteration * n + next set vector]
    vpminud ymm14, ymm13, ymm12 ;Find minimum value in double word values between ymm13 and ymm12 and pass them to ymm14

    lea r12, [rcx + r12]                ;find address of resultGraph[i * n + next set vector]
    vmovdqu ymmword ptr [r12], ymm14    ;write data from ymm14 to resultGraph[i * n + next set vector]

    vpxor ymm14, ymm14, ymm14           ;clear ymm14 register
    vpxor ymm13, ymm13, ymm13           ;clear ymm13 register

    add r8, 8
    cmp r8, n       ;if vertex is longer than 8 repeat iteration for the same r11 value
    je reset_r_eight
    jl inner_loop_start

inner_loop_increment:
    inc     rbx
    cmp     rbx, n
    je      loop_end
    jl      inner_loop_start

reset_r_eight:
    xor r8, r8
    jmp inner_loop_increment

loop_end:
	ret
    

asmFloydWarshall endp
end